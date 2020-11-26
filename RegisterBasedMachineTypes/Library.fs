namespace RegisterBasedMachineTypes

module Types =
    
    type Register =
        | R0
        | R1
        | R2
        | R3
        | R4
        | R5
        | R6
        | R7
        | PC

    
    type ConstObj = {value:int; destinationRegister:Register}
    type AddObj = {aRegister:Register; bRegister:Register; destinationRegister:Register}
    type SubObj = {aRegister:Register; bRegister:Register; destinationRegister:Register}
    type StoreObj = {srcRegister:Register; baseSlotRegister:Register; slot_offset:int}

    type Instruction =
        //| CONST of int * Register
        //| ADD of Register * Register * Register
        | CONST of ConstObj
        | ADD of AddObj
        | SUB of SubObj
        | STORE of StoreObj
        | HALT

