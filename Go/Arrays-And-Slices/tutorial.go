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

	fmt.Println("arr2 references arr and arr2[0] is set to 17")
	arr2 := arr
	arr2[0] = 17
	fmt.Printf("arr: %v\n", arr)
	fmt.Printf("arr2: %v\n", arr2)

	fmt.Println("Creating arrArr and arrArr2 as {arr,arr2}")
	arrArr := [2][5]int{arr, arr2}
	arrArr2 := [2][5]int{arr, arr2}

	fmt.Println("Arrays are values >>> Deep Copy")
	fmt.Println("Assigning arrArr[0][0] to 23")
	arrArr[0][0] = 23
	fmt.Printf("arrArr: %v\n", arrArr)
	fmt.Printf("arrArr2: %v\n", arrArr2)
	fmt.Printf("arr: %v\n", arr)
	fmt.Printf("arr2: %v\n", arr2)

	fmt.Println("Assigning arrArr[0][0] to 37")
	arrArr2[1][0] = 37
	fmt.Printf("arrArr: %v\n", arrArr)
	fmt.Printf("arrArr2: %v\n", arrArr2)
	fmt.Printf("arr: %v\n", arr)
	fmt.Printf("arr2: %v\n", arr2)
}

func print(msg string, arr [5]int, slice []int) {
	fmt.Println(msg)
	fmt.Printf("arr: %v\n", arr)
	fmt.Printf("slice: %v\n\n", slice)
}
