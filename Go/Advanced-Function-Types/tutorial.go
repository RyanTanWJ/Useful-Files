package main

import (
	"fmt"
)

// ENTRY POINT
func main() {
	a := 16
	foo := func(bar int) int {
		fmt.Printf("bar: %v\n", bar)
		bar += 5
		fmt.Printf("foo(bar): %v\n", bar)
		return bar
	}

	fmt.Printf("foo(2): %v\n", foo(2))
	fmt.Printf("a: %v, foo(a): %v\n", a, foo(a))

	foo2 := func(bar string) string {
		bar = "This is " + bar
		fmt.Printf("foo2 says: %s\n", bar)
		return bar
	}("A String")

	fmt.Printf("foo2 value: %v\n", foo2)

	foo3(foo)
	foo4()(foo)
}

func foo3(myFunc func(int) int) {
	bar := myFunc(-3) + myFunc(2)
	fmt.Printf("foo3 says: %v\n", bar)
}

func foo4() func(func(int) int) {
	fmt.Println(">>> FOO4 CALLED! <<<")
	return foo3
}
