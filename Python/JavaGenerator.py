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
        setMethodFields = ""
        getStringFields = ""
        for field in self.fields:
            fieldDeclarations += field.getDeclarationString()
            getMethodFields += field.getGetBytesString()
            setMethodFields += field.getSetBytesString()
            getStringFields += field.getGetStringString()

        classStr += fieldDeclarations
        
        classStr += "\n" + self.getConstructor()

        classStr += self.getGetBytesLine(getMethodFields) + "\n"

        classStr += self.getSetBytesLine(setMethodFields) + "\n"

        classStr += self.getGetStringLine(getStringFields)
        
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
        retVal += "public void getBytes(ByteBuffer bb) {\n"
        retVal += fields
        retVal += ((self.indentLevel + 1) * "    ")
        retVal += "}\n"
        return retVal
    
    def getSetBytesLine(self, fields):
        retVal = ((self.indentLevel + 1) * "    ")
        retVal += "public void setBytes(ByteBuffer bb) {\n"
        retVal += ((self.indentLevel + 2) * "    ")
        retVal += "int idx = 0;\n"
        retVal += ((self.indentLevel + 2) * "    ")
        retVal += "byte[] tempByteArr;\n\n"
        retVal += fields
        retVal += ((self.indentLevel + 1) * "    ")
        retVal += "}\n"
        return retVal

    def getGetStringLine(self, fields):
        retVal = ((self.indentLevel + 1) * "    ")
        retVal += "@Override\n"
        retVal += ((self.indentLevel + 1) * "    ")
        retVal += "public String getString() {\n"
        retVal += ((self.indentLevel + 2) * "    ")
        retVal += "StringBuilder sb = new StringBuilder();\n"
        retVal += fields
        retVal += ((self.indentLevel + 2) * "    ")
        retVal += "return sb.toString();\n"
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
            "ushort": ["bb.putShort((new Integer(", ")).shortValue());\n"],
            "spare": ["bb.put(new byte[]{", "});\n"],
            "String": ["bb.put(StandardCharsets.US_ASCII.encode(", ").array());\n"],
            "byte": ["bb.put(", ");\n"],
        }

        retVal = ((self.indentLevel + 1) * "    ") + lineDict[self.fieldtype][0]
        if (self.fieldtype == "spare"):
            retVal += (("(byte) 0," * self.length)[:-1])
        else:
            retVal += self.name
        return retVal + lineDict[self.fieldtype][1]

    def getSetBytesString(self):
        # Spares, Strings and Unsigned Shorts are too unique
        lineDict = {
            "integer": ["bb.getInt(", "idx);\n", "4"],
            "short": ["bb.getShort(", "idx);\n", "2"],
            "float": ["bb.getFloat(", "idx);\n", "4"],
            "uinteger": ["bb.getInt(", "idx);\n", "4"],
            "byte": ["bb.get(", "idx);\n", "1"]
        }

        if (self.fieldtype == "spare"):
            retVal = ((self.indentLevel + 1) * "    ") + "//" + self.name
            retVal += "\n" + ((self.indentLevel + 1) * "    ") + "idx += " +\
                      str(self.length) + ";\n\n"
            return retVal
        elif (self.fieldtype == "String"):
            retVal = ((self.indentLevel + 1) * "    ") +\
                     "tempByteArr = new byte[" + str(self.length) + "];\n"
            retVal += ((self.indentLevel + 1) * "    ") +\
                     "bb.get(tempByteArr, idx, idx + " + str(self.length) + ");\n"
            retVal += ((self.indentLevel + 1) * "    ") + self.name +\
                      " = new String(tempByteArr, StandardCharsets.US_ASCII);\n"
            retVal += ((self.indentLevel + 1) * "    ") + "idx += " + str(self.length) + ";\n\n"
            return retVal
        elif (self.fieldtype == "ushort"):
            retVal = ((self.indentLevel + 1) * "    ") + "tempByteArr = new byte[4];\n"
            retVal += ((self.indentLevel + 1) * "    ") + "tempByteArr[2] = bb.get(idx);\n"
            retVal += ((self.indentLevel + 1) * "    ") + "tempByteArr[3] = bb.get(idx+1);\n"
            retVal += ((self.indentLevel + 1) * "    ") + self.name +\
                      " = ByteBuffer.wrap(tempByteArr).getInt();\n"
            retVal += ((self.indentLevel + 1) * "    ") + "idx += 2;\n\n"
            return retVal
        
        retVal = ((self.indentLevel + 1) * "    ") + self.name
        retVal += " = " + lineDict[self.fieldtype][0]
        retVal += lineDict[self.fieldtype][1]
        retVal += ((self.indentLevel + 1) * "    ")
        retVal += "idx += " + lineDict[self.fieldtype][2] + ";\n\n"
        return retVal

    def getGetStringString(self):
        # Spares, Strings and Unsigned Shorts are too unique
        lineDict = {
            "integer": ['String.format("', ': %d\\n", ', '));\n'],
            "short": ['String.format("', ': %d\\n", ', '));\n'],
            "float": ['String.format("', ': %d\\n", ', '));\n'],
            "uinteger": ['String.format("', ': %s\\n", Integer.toUnsignedString(', ')));\n'],
            "ushort": ['String.format("', ': %d\\n", ', '));\n'],
            "String":  ['String.format("', ': %s\\n", ', '));\n'],
            "byte":  ['String.format("', ': %d\\n", ', '));\n'],
        }

        retVal = ((self.indentLevel + 1) * "    ") + 'sb.append('
        if (self.fieldtype == "spare"):
            return  retVal + '"' + self.name + '\\n");\n'
        retVal += lineDict["integer"][0] + self.name
        retVal += lineDict["integer"][1] + self.name
        retVal += lineDict["integer"][2]
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
