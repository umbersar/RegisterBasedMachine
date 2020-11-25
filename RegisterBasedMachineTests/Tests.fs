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
                    
                    Instruction.CONST{value=1;destinationRegister=Register.R1};
                    Instruction.CONST{value=2; destinationRegister=Register.R2};
                    Instruction.ADD{aRegister=Register.R1; bRegister=Register.R2; destinationRegister=Register.R3};
                    Instruction.Halt;
                  ]
    let program = new List<Instruction>(program1)

    let m = Machine program

    Assert.True(true)
