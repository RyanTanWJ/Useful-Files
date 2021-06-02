package ryan.useful;

import java.nio.ByteBuffer;

public class UnsignedShortTest{
    public static void main(String []args){
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

    public static class UnShort{
        private int s;

        public UnShort(String unShort){
            s = Integer.parseInt(unShort);
        }

        public UnShort(short unShort){
            byte[] arr = new byte[4];
            ByteBuffer bb = ByteBuffer.wrap(arr);
            bb.put(new byte[] {(byte) 0, (byte) 0});
            bb.putShort(unShort);
            s = bb.getInt(0);
        }

        public UnShort(int unShort){
            s = unShort;
        }

        public UnShort(){
            s = 0;
        }

        public int value(){
            return s;
        }

        public short shortValue(){
            return (new Integer(s)).shortValue();
        }

        public byte[] getBytes(){
            byte[] arr = new byte[2];
            ByteBuffer bb = ByteBuffer.wrap(arr);
            bb.putShort((new Integer(s)).shortValue());
            return bb.array();
        }

        @Override
        public String toString(){
            return "" + s;
        }
    }
}
