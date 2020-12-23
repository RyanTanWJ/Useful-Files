package main

import (
	"fmt"
)

// ENTRY POINT
func main() {
	var arr [5]int = [5]int{1, 2, 3, 4, 5}
	var slice []int = arr[1:4]

	msg := "Start"
	print(msg, arr, slice)

	msg = "Slice index 0 modified"
	slice[0] = 0
	print(msg, arr, slice)

	msg = "Append 10 to slice"
	slice = append(slice, 10)
	print(msg, arr, slice)

	msg = "Append 12 and 100 to slice"
	slice = append(slice, 12)
	slice = append(slice, 100)
	print(msg, arr, slice)

	msg = "Slice index 0 modified again"
	slice[0] = 20
	print(msg, arr, slice)
}

func print(msg string, arr [5]int, slice []int) {
	fmt.Println(msg)
	fmt.Printf("arr: %v\n", arr)
	fmt.Printf("slice: %v\n\n", slice)
}
