package main

import (
	"crypto/aes"
	"crypto/cipher"
	"crypto/md5"
	"crypto/rand"
	"encoding/hex"
	"fmt"
	"io"
	"io/ioutil"
	"os"
)

const (
	textToEncrypt = "Encrypt this"
	password      = "password"
)

func createHash(key string) string {
	hasher := md5.New()
	hasher.Write([]byte(key))
	return hex.EncodeToString(hasher.Sum(nil))
}

func encrypt(data []byte, passphrase string) []byte {
	hashedPass := createHash(passphrase)

	// Create cipher
	block, err := aes.NewCipher([]byte(hashedPass))
	if err != nil {
		panic(err)
	}

	gcm, err := cipher.NewGCM(block)
	if err != nil {
		panic(err)
	}

	//Create nonce
	nonce := make([]byte, gcm.NonceSize())
	_, err = io.ReadFull(rand.Reader, nonce)
	if err != nil {
		panic(err)
	}
	ciphertext := gcm.Seal(nonce, nonce, data, nil)
	return ciphertext
}

func decrypt(data []byte, passphrase string) []byte {
	hashedPass := createHash(passphrase)
	key := []byte(hashedPass)

	// Create cipher
	block, err := aes.NewCipher([]byte(key))
	if err != nil {
		panic(err)
	}

	gcm, err := cipher.NewGCM(block)
	if err != nil {
		panic(err)
	}

	nonce, ciphertext := data[:gcm.NonceSize()], data[gcm.NonceSize():]
	plaintext, err := gcm.Open(nil, nonce, ciphertext, nil)
	if err != nil {
		panic(err)
	}
	return plaintext
}

func encryptFile(filename string, data []byte, passphrase string) {
	f, _ := os.Create(filename)
	defer f.Close()
	f.Write(encrypt(data, passphrase))
}

func decryptFile(filename string, passphrase string) []byte {
	data, _ := ioutil.ReadFile(filename)
	return decrypt(data, passphrase)
}

// Adapted from https://www.youtube.com/watch?v=jgTqR8PuWuU
func main() {
	// Encrypt
	ciphertext := encrypt([]byte("Encrypt this"), password)
	fmt.Printf("My encrypted plaintext:\n%s\n", ciphertext)

	// Decrypt
	plaintext := decrypt(ciphertext, password)
	fmt.Printf("My decrypted ciphertext:\n%s\n", string(plaintext))
}
