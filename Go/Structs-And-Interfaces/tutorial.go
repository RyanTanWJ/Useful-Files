package main

import "fmt"

// ENTRY POINT
func main() {
	v1 := Vector3{1, 2, 3}
	v2 := Vector3{y: -2, z: -1}

	fmt.Printf("Type of v1: %T, Value of v1: %v\nType of v2: %T, Value of v2: %v\n", v1, v1, v2, v2)

	changeXByVal(v1, -3.09)
	fmt.Printf("Type of v1: %T, Value of v1: %v\nType of v2: %T, Value of v2: %v\n", v1, v1, v2, v2)
	changeXByPtr(&v2, 1.05)
	fmt.Printf("Type of v1: %T, Value of v1: %v\nType of v2: %T, Value of v2: %v\n", v1, v1, v2, v2)

	fmt.Printf("v1.getX() = %v\n", v1.getX())
	fmt.Printf("v1.setX(5.45)\n")
	v1.setX(5.45)
	fmt.Printf("Type of v1: %T, Value of v1: %v\nType of v2: %T, Value of v2: %v\n", v1, v1, v2, v2)

	s1 := Sphere{"Sphere1", 10.3, &v1}
	fmt.Println(s1)

	c1 := Cube{"Cube1", 7.8, &v2}
	fmt.Println(c1)

	slice := []NamedObject{s1, c1}

	for idx, obj := range slice {
		fmt.Printf("%v. %s\n", idx+1, obj.GetName())
	}
}

type Vector3 struct {
	x, y, z float32
}

// Creates a deep copy of Vector3
func changeXByVal(v Vector3, val float32) {
	v.x = val
}

// Modifies the Vector3 passed as argument
func changeXByPtr(v *Vector3, val float32) {
	v.x = val
}

func (v Vector3) getX() float32 {
	return v.x
}

func (v *Vector3) setX(val float32) {
	v.x = val
}

type NamedObject interface {
	GetName() string
}

type Sphere struct {
	name   string
	radius float32
	center *Vector3
}

type Cube struct {
	name   string
	length float32
	center *Vector3
}

func (s Sphere) GetName() string {
	return s.name
}

func (c Cube) GetName() string {
	return c.name
}

// Overrides string representation of Vector3
func (v Vector3) String() string {
	return fmt.Sprintf("{x: %v; y: %v; z: %v}", v.x, v.y, v.z)
}

// Overrides string representation of Sphere
func (s Sphere) String() string {
	return fmt.Sprintf("%s has radius, %v, and center, %v.", s.GetName(), s.radius, *s.center)
}

// Overrides string representation of Cube
func (c Cube) String() string {
	return fmt.Sprintf("This is %s, a Cube of length %v and center %v.", c.GetName(), c.length, c.center)
}
