module Tests

open System
open Xunit
open RegisterBasedMachineTypes.Types
open RegisterBasedMachine
open System.Collections.Generic

[<Fact>]
let ``My test`` () =
    Assert.True(true)

[<Fact>]
let ``Addition`` () =

    let program1 = [
                    //Instruction.CONST(1, Register.R1);
                    //Instruction.CONST(2, Register.R2);
                    //Instruction.ADD( Register.R1, Register.R2, Register.R3);
                    
                    Instruction.CONST{value=3;destinationRegister=Register.R1};
                    Instruction.CONST{value=4; destinationRegister=Register.R2};
                    Instruction.ADD{aRegister=Register.R1; bRegister=Register.R2; destinationRegister=Register.R1};
                    
                    Instruction.CONST{value=5; destinationRegister=Register.R2};
                    Instruction.SUB{aRegister=Register.R1; bRegister=Register.R2; destinationRegister=Register.R1};
                    
                    Instruction.STORE{srcRegister=Register.R1; baseSlotRegister=Register.R0; slot_offset=RegisterBasedMachine.Memory.IO_OUT_Index};
                    Instruction.HALT;
                  ]
    let program = new List<Instruction>(program1)

    let m = Machine program
    m.Run()

    Assert.True(true)
