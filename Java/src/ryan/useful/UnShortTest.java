package ryan.useful;

import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.*;

class UnShortTest {
    private UnShort us;

    @BeforeEach
    public void setUp() throws Exception {
        short s = -1;
        us = new UnShort(s);
    }

    @Test
    @DisplayName("Int Value")
    public void testIntValue() {
        int expected = 65535;
        assertEquals(expected, us.value(), "Underlying unsigned short uses an int");
    }

    @Test
    @DisplayName("Short Value")
    public void testShortValue() {
        short expected = -1;
        assertEquals(expected, us.shortValue(), "shortValue() should convert underlying int to short");
    }

}