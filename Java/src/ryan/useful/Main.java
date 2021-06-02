package ryan.useful;

import java.nio.ByteBuffer;

public class Main {

    public static void main(String[] args) {
        run();
    }

    public static void run(){
        short s = -5535;
        UnShort us = new UnShort(s);
        System.out.println(s);
        System.out.println(us);

        byte[] arr = new byte[2];
        ByteBuffer bb = ByteBuffer.wrap(arr);
        bb.putShort((new Integer(s)).shortValue());

        String line = "";
        for (byte b : bb.array()){
            line += b + " ";
        }
        line += "\n";
        for (byte b: us.getBytes()){
            line += b + " ";
        }
        System.out.println(line);
    }
}
