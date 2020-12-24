package main

import (
	"access/access"
	"fmt"
)

// ENTRY POINT
func main() {
	fmt.Println()
	access.AccessMethod()
	// access.noAccessMethod() // This method cannot be accessed as it is not exported
}
