package main

import "fmt"

// ENTRY POINT
func main() {
	var myMap map[string]int = make(map[string]int) // empty map
	myMap2 := map[string]int{"foo": 1, "bar": 2}    // Populated map
	var myMap3 map[string]int                       // nil map

	fmt.Printf("My Map: %v\n", myMap)
	fmt.Printf("My Map2: %v\n", myMap2)
	fmt.Printf("My Map3: %v\n", myMap3)
	fmt.Printf("My Map3 is nil: %v\n", myMap3 == nil)

	// Adding
	fmt.Println("Assigned \"try\" to 1 in My Map")
	myMap["try"] = 1
	fmt.Printf("My Map: %v\n", myMap)

	fmt.Println("Assigned \"this\" to 2 in My Map")
	myMap["this"] = 2
	fmt.Printf("My Map: %v\n", myMap)

	fmt.Println("Assigned \"out\" to 3 in My Map")
	myMap["out"] = 3
	fmt.Printf("My Map: %v\n", myMap)

	// Modifying
	fmt.Println("Modified \"foo\" to 3 in My Map 2")
	myMap2["foo"] = 3
	fmt.Printf("My Map2: %v\n", myMap2)

	// Removing
	fmt.Println("Removed \"bar\" from My Map 2")
	delete(myMap2, "bar")
	fmt.Printf("My Map2: %v\n", myMap2)

	// Assigning to nil map
	// myMap3["this"] = 3 // This line throws an error
	fmt.Println("This line throws an error: myMap3[\"this\"] = 3")

	// Iterating over maps
	fmt.Println("Iterating and printing key-value pairs in My Map")
	for k, v := range myMap {
		fmt.Printf("Key: %s, Value: %v\n", k, v)
	}

	fmt.Println("Iterating and printing keys in My Map")
	for k := range myMap {
		fmt.Printf("Key: %s\n", k)
	}
}
