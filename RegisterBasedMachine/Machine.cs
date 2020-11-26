using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RegisterBasedMachineTypes.Types;

namespace RegisterBasedMachine {

    public class Machine {
        //total of 9 registers with one being the program counter. Register R0 is hardwired to always contains the value 0 which is the base address of our memory block. Register R7 is initialized 
        //to the highest valid memory address. A special register PC holds the index of the next instruction that will execute.
       //All the registers can store int values. Those values can either be intermediary values of a calculation or the slot index into memory address space. But R0, R7 and PC are special that they can not only values to 
        Dictionary<Register, int> Registers;
        private Memory memory;


        bool IsRunning = false;
        public Machine(List<Instruction> program) {
            //initialize the registers to 0
            this.Registers = new Dictionary<Register, int> { { Register.R0, 0 }, { Register.R1, 0 }, { Register.R2, 0 }, { Register.R3, 0 }, { Register.R4, 0 }, { Register.R5, 0 },
                                                                { Register.R6, 0 }, { Register.R7, 0 }, { Register.PC, 0 } };

            memory = new Memory();
            memory.LoadInstructions(program);

            this.Registers[Register.R0] = Memory.memory_start_Index;//R0 is used not to store the a value but to hold the base address of memory
            this.Registers[Register.R7] = Memory.memory_end_Index;//register R7 holds the initialized with the highest valid memory index (second last memory slot as last slot is mapped to terminal).
        }

        public void Run() {
            this.IsRunning = true;

            while (IsRunning) {
                Instruction instruction = this.memory.Instructions[this.Registers[Register.PC]];
                this.Registers[Register.PC] += 1;//move counter to next statement
                Eval(instruction);

                this.Registers[Register.R0] = Memory.memory_start_Index;//even if the R0 is used for intermediary calculation, always reset it before starting execution of next statement
                this.Registers[Register.R7] = Memory.memory_end_Index;//even if the R7 is used for intermediary calculation, always reset it before starting execution of next statement
            }
        }

        private void Eval(Instruction instruction) {
            switch (instruction) {
                case Instruction.CONST c:
                    this.Registers[c.Item.destinationRegister] = c.Item.value;
                    break;
                case Instruction.ADD a:
                    this.Registers[a.Item.destinationRegister] = this.Registers[a.Item.aRegister] + this.Registers[a.Item.bRegister];
                    break;
                case Instruction.SUB sb:
                    this.Registers[sb.Item.destinationRegister] = this.Registers[sb.Item.aRegister] - this.Registers[sb.Item.bRegister];
                    break;
                case Instruction.STORE s:
                    int store_slot_index = this.Registers[s.Item.baseSlotRegister] + s.Item.slot_offset;
                    this.memory.SetValue(this.Registers[s.Item.srcRegister], store_slot_index);
                    break;
                case Instruction h when h.IsHALT:
                    this.IsRunning = false;
                    break;
                default:
                    break;
            }
        }
    }
}
