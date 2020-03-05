import json
import os

javaImports = {"java.nio.ByteBuffer","java.nio.charset.StandardCharsets"}
varNames = {}

##Python 2 Class definition
##class JavaClass(object):
class JavaClass:

    def __init__(self, name, fields, indentLevel):
        self.name = name.title()
        self.indentLevel = indentLevel
        self.baseLength = 0
        self.processFields(fields)

    def processFields(self, fields):
        self.fields = []
        for field in fields:
            fieldName = field["name"]
            fieldType = field["type"]
            fieldValue = field["value"]
            
            if (fieldType == "Class"):
                print("innerClass")
            else:
                self.baseLength += fieldValue
                f = Field(fieldName, fieldType, fieldValue, self.indentLevel+1)
            self.fields.append(f)

    def toString(self):
        classStr = self.getHeader()
        
        if (self.indentLevel==0):
            importLines = ""
            for i in javaImports:
                importLines += "import " + i + ";\n"
            classStr = importLines + "\n" + classStr

        classStr += self.getLengthLine()
        
        fieldDeclarations = ""
        getMethodFields = ""
        for field in self.fields:
            fieldDeclarations += field.getDeclarationString()
            getMethodFields += field.getGetBytesString()

        classStr += fieldDeclarations
        
        classStr += "\n" + self.getConstructor()

        classStr += self.getGetBytesLine(getMethodFields)
        
        classStr += (self.indentLevel * "    ") + "}"
        return classStr

    def getHeader(self):
        retStr = (self.indentLevel * "    ") + "public "
        if (self.indentLevel > 0):
            retStr += "static "
        retStr += "class "
        retStr += self.name + " {\n"
        return retStr

    def getLengthLine(self):
        return ((self.indentLevel + 1) * "    ") +\
               "public static final int length = " +\
               str(self.baseLength) + ";\n\n"
    
    def getConstructor(self):
        return ((self.indentLevel + 1) * "    ") + "public " + self.name + "() {}\n\n"

    def getGetBytesLine(self, fields):
        retVal = ((self.indentLevel + 1) * "    ")
        retVal += "public getBytes(ByteBuffer bb) {\n"
        retVal += fields
        retVal += ((self.indentLevel + 1) * "    ")
        retVal += "}\n"
        return retVal
        
##Python 2 Class definition
##class Field(object):
class Field:

    typeDict = {
        "integer":{"type":"int", "access":"public", "length":4},
        "short":{"type":"short", "access":"public", "length":2},
        "float":{"type":"float", "access":"public", "length":4},
        "uinteger":{"type":"int", "access":"public", "length":4},
        "ushort":{"type":"int", "access":"public", "length":2},
        "spare":{"type":"int", "access":"private final", "length":None},
        "String":{"type":"String", "access":"public", "length":None},
        "byte":{"type":"byte", "access":"public", "length":1}
    }

    def __init__(self, name, datatype, length, indentLevel):
        self.name = Field.getValidName(name, datatype)
        self.fieldtype = datatype
        self.declaretype = Field.findType(datatype)
        self.accessLevel = Field.findAccessLevel(datatype)
        self.length = Field.findLength(datatype, length)
        self.indentLevel = indentLevel

    def getDeclarationString(self):
        fieldStr = (self.indentLevel * "    ")
        fieldStr += self.accessLevel + " "
        fieldStr += self.declaretype + " "
        fieldStr += self.name
        if (self.fieldtype == "spare"):
            fieldStr += " = " + str(self.length)
        return fieldStr + ";\n"

    def getGetBytesString(self):
        lineDict = {
            "integer": ["bb.putInt(", ");\n"],
            "short": ["bb.putShort(", ");\n"],
            "float": ["bb.putFloat(", ");\n"],
            "uinteger": ["bb.putInt(", ");\n"],
            "ushort": ["bb.putShort((new Integer(", ")).shortValue();\n"],
            "spare": ["bb.put(new byte[]{", "});\n"],
            "String": ["bb.put(StandardCharsets.US_ASCII.encode(", ").array();\n"],
            "byte": ["bb.put(", "});\n"],
        }

        retVal = ((self.indentLevel + 1) * "    ") + lineDict[self.fieldtype][0]
        if (self.fieldtype == "spare"):
            retVal += (("(byte) 0," * self.length)[:-1])
        else:
            retVal += self.name
        return retVal + lineDict[self.fieldtype][1]

    def getSetBytesString(self):
        retVal = ((self.indentLevel + 1) * "    ")
        return retVal

    @staticmethod
    def getValidName(name, datatype):
        retVal = ""
        if (datatype == "spare"):
            retVal = "spare"
        elif (datatype == "uinteger"):
            retVal = "u_" + name
        elif (datatype == "ushort"):
            retVal = "us_" + name
        else:
            retVal = name
        
        if retVal in varNames:
            varNames[retVal] += 1
            return retVal + str(varNames[retVal])
        else:
            varNames[retVal] = 1
            return retVal

    @staticmethod
    def findType(fieldType):
        return Field.typeDict[fieldType]["type"]

    @staticmethod
    def findAccessLevel(fieldType):
        return Field.typeDict[fieldType]["access"]

    @staticmethod
    def findLength(fieldType, value):
        if (Field.typeDict[fieldType]["length"] == None):
            return value
        return Field.typeDict[fieldType]["length"]

######## MAIN METHOD ########
def main():
    currDir = os.getcwd()
    jsonFilePath = currDir + "\\TestInput.json"

    with open(jsonFilePath) as f:
      data = json.load(f)

    for c in data["Classes"]:
        if (c["type"]=="Class"):
            jc = JavaClass(c["name"], c["value"], 0)
            print(jc.toString())
            
main()
