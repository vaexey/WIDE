## Instruction
###### Custom instruction syntax

#### Sample instruction with explicit forks

```C
// Instruction JUMP IF ZERO (example flag Z)
instruction(JZ)
{
    // Implementation defines instruction syntax
    // eg. "JZ 05h"
    impl(JZ, $int(REG_IAD))
    {
        // How is instruction machine code concatenated
        $1, OPCODE
    };

    // Define fork value generator (default 0)
    fork(Z);

    // Defines signals for fork value 0 (default)
    signals()
    {
        czyt wys wei il;
        wyl wea;
    }

    // Defines signals for fork value 1
    signals(1)
    {
        czyt wys wei il;
        wyad wea wel;
    }
}
```

#### Sample instruction with generated forks

```C
// Instruction JUMP IF ZERO (example flag Z)
instruction(JZ)
{
    // Implementation defines instruction syntax
    // eg. "JZ 05h"
    impl(JZ, $int(REG_IAD))
    {
        // How is instruction machine code concatenated
        $1, OPCODE
    };
    
    // Defines signals in a more complex format
    // for the assembler to create forks on its own
    code()
    {
        czyt wys wei il;
        if(Z)
        {
            wyad wea wel;
        }
        else
        {
            wyl wea;
        }
    }
}
```

#### Example keyword bound to two instructions

```C
// Instruction MOVE IMMEDIATE TO AK
instruction(MOV_AK_IMM)
{
    impl(MOV, AK, $int(REG_IAD))
    {
        $2, OPCODE
    };

    signals()
    {
        czyt wys wei il;
        wyad as weja przp weak;
        wyl wea;
    }
}

// Instruction MOVE IMMEDIATE TO X
instruction(MOV_AK_IMM)
{
    impl(MOV, AK, $int(REG_IAD))
    {
        $2, OPCODE
    };

    signals()
    {
        czyt wys wei il;
        wyad as wex;
        wyl wea;
    }
}
```