using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RegisterBasedMachineTypes.Types;

namespace RegisterBasedMachine {

    public class Machine {
        //total of 9 registers with one being the program counter. Register R0 is hardwired to always contains the value 0. Register R7 is initialized to the highest valid memory
        //address. A special register PC holds the index of the next instruction that will execute.
        Dictionary<Register, int> Registers;

        //The memory of the machine consists of 65536 memory slots,each of which can hold an integer value.  Special LOAD/STORE
        //instructions access the memory.  Instructions are stored separately.  All memory addresses from 0-65535 may be used.
        const int total_memory_slots = 65536;
        List<int> Memory;

        List<Instruction> Instructions;

        //The machine has a single I/O port which is mapped to the memory address 65535 (0xFFFF).  The symbolic constant IO_OUT contains the value 65535 and can be used when writing code.  
        //Writing an integer to this address causes the integer value to be printed to terminal. This can be useful for debugging.
        const int IO_OUT = total_memory_slots - 1;

        bool IsRunning = false;
        public Machine(List<Instruction> program) {
            //initialize the registers to 0
            this.Registers = new Dictionary<Register, int> { { Register.R0, 0 }, { Register.R1, 0 }, { Register.R2, 0 }, { Register.R3, 0 }, { Register.R4, 0 }, { Register.R5, 0 },
                                                                { Register.R6, 0 }, { Register.R7, 0 }, { Register.PC, 0 } };

            //Total memory is 65536 slot each of which can hold an integer value. The last slot in the memory is mapped to IO, in this case it prints to terminal
            this.Memory = new List<int>(total_memory_slots);

            this.Registers[Register.R0] = 0;
            this.Registers[Register.R7] = total_memory_slots - 2;//register R7 holds the initialized with the highest valid memory index (second last memory slot as last is mapped to terminal).

            this.Instructions = program;
        }

        public void Run() {
            this.IsRunning = true;

            while (IsRunning) {
                Instruction instruction = this.Instructions[this.Registers[Register.PC]];
                this.Registers[Register.PC] += 1;//move counter to next statement
                Eval(instruction);

                this.Registers[Register.R0] = 0;//even if the R0 is used for intermediary calculation, always reset it before starting execution of next statement
            }
        }

        private void Eval(Instruction instruction) {
            switch (instruction) {
                case Instruction.CONST c:
                    this.Registers[c.Item.destinationRegister] = c.Item.value;
                    break;
                default:
                    break;
            }
        }
    }
}
