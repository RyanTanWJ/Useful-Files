package main

import (
	"compress/gzip"
	"fmt"
	"os"
)

const (
	textToCompress = "This is my text" // Text to compress
	fileName       = "/Go/Compression/Gzip/MyCompressedFile.gz"
)

// Adapted from https://www.dotnetperls.com/compress-go
func main() {
	// Init file path
	filepath, err := os.Getwd()
	if err != nil {
		panic(err)
	}
	filepath += fileName
	fmt.Println(filepath)

	// Open a file for writing.
	wf, err := os.Create(filepath)
	if err != nil {
		panic(err)
	}

	// Create gzip writer.
	w := gzip.NewWriter(wf)

	// Write bytes in compressed form to the file.
	w.Write([]byte(textToCompress))

	// Close the file.
	w.Close()

	// Open the gzip file.
	rf, err := os.Open(filepath)
	if err != nil {
		panic(err)
	}

	// Create new reader to decompress gzip.
	reader, err := gzip.NewReader(rf)
	if err != nil {
		panic(err)
	}

	// Empty byte slice.
	result := make([]byte, len(textToCompress))

	// Read in data.
	count, _ := reader.Read(result)
	reader.Close()

	// Print our decompressed data.
	fmt.Println(count)
	fmt.Println(string(result))
}
