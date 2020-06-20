# Performance difference between BitMask and List
To determine the performance difference between using a List of enumerations and a BitMask of the same enumeration, a series of tests were written, and the Unity Profiler was used to determine the time required to execute them.

To run the script:
1. In a new scene in Unity, create a new empty GameObject
2. Attach EnumTest.cs to the newly created GameObject.
3. Open the Profiler by navigating to Window > Analysis > Profiler
4. Run the scene.
5. Press "T" to run the test

The profiler data is in TestData.data.

### Test 1
Check an object for an enum which exists within it.
![Test 1 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test1.png)

### Test 2
Check an object for 2 enums of which one exists within it and the other does not.
![Test 2 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test2.png)

### Test 3
Check an objects for 2 enums that exist within it. This test also includes a different implementation of the constructor for BitMasks (see BitMaskTest3Alt method).
![Test 3 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test3.png)

### Test 4
Check 3 objects for an enum that does not exist in the list.
![Test 4 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test4.png)

### Test 5
Check an object with all enums for the first, middle and last enum.
![Test 5 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test5.png)

### Test 6
Check 8 objects for the middle enum.
![Test 6 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test6.png)

### Test 7
Get all Objects with a certain enum.
![Test 7 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test7.png)

### Test 8
Get all Objects with certain 2 enums.
![Test 8 Results Screenshot](https://github.com/RyanTanWJ/Useful-Files/blob/master/C%23/Unity/BitMaskPerformance/Test8.png)
