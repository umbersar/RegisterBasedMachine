using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RegisterBasedMachineTypes.Types;

namespace RegisterBasedMachine {
    public class Memory {

        //total memory in bytes. This is 64kb
        public static int Total_Memory = 65536;

        //we will use the memory to only store int datatypes. Each int is 4 bytes long. So we can slot our memory taking into account this premise.
        //instructions are stored separately
        List<int> memory_slots;

        static int total_memory_slots = Total_Memory / sizeof(int);

        public static int IO_OUT_Index = total_memory_slots - 1;//value written in the last memory slot would be printed
        public static int memory_start_Index = 0;
        public static int memory_end_Index = total_memory_slots - 2;

        public List<Instruction> Instructions { get; private set; }

        public Memory() {
            int slots_count = Total_Memory / sizeof(int);
            this.memory_slots = new List<int>();
            this.memory_slots.AddRange( Enumerable.Repeat(0, slots_count));
        }

        public void LoadInstructions(List<Instruction> instructions) {
            this.Instructions = instructions;
        }

        public int GetValue(int slot_index) {
            return memory_slots[slot_index];
        }

        public void SetValue(int value, int slot_index) {
            memory_slots[slot_index] = value;
            if (slot_index == IO_OUT_Index)
                Console.WriteLine(value);
        }
    }
}
