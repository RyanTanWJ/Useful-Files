package main

import (
	"fmt"
)

// ENTRY POINT
func main() {
	x := 7
	fmt.Println("Assigning y the dereferenced value of x")
	y := &x
	fmt.Printf("Type of x: %T, Value of x: %v\nType of y: %T, Value of y: %v\n", x, x, y, y)

	fmt.Printf("Assigning the reference of y the value of 8\n")
	*y = 8
	fmt.Printf("Type of x: %T, Value of x: %v\nType of y: %T, Value of y: %v\n", x, x, y, y)

	fmt.Printf("Changing y with the changeVal function\n")
	changeVal(y, 15)
	fmt.Printf("Type of x: %T, Value of x: %v\nType of y: %T, Value of y: %v\n", x, x, y, y)

	fmt.Printf("Changing address of x with the changeVal function\n")
	changeVal(&x, 10)
	fmt.Printf("Type of x: %T, Value of x: %v\nType of y: %T, Value of y: %v\n", x, x, y, y)
}

func changeVal(changeMe *int, changeTo int) {
	*changeMe = changeTo
}
