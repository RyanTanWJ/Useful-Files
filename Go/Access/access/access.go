package access

import "fmt"

type Access struct {
	accessible bool
}

type noAccess struct {
	accessible bool
}

func AccessMethod() {
	fmt.Println("AccessMethod can be accessed!")
}

func noAccessMethod() {
	fmt.Println("noAccessMethod can be accessed!")
}
