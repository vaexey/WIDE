## Flow
###### Data flow in emulator

#### Memory structure

Memory is treated as a set of memory endpoints, which can be read and written.
Endpoints are divided into three categories:  
a. Register:  
  During execution cycle, provides constant read value, regardless of
values being written to it. In the next cycle, written value is passed further.  
b. Bus:  
  During execution cycle, reading from bus must be preceded with a write operation
c. Disjointed:  
  Offering only a write or a read endpoint thus eliminating synchronization problems

#### Execution cycle

**Step 1.** An instruction is executed.  
It consists of an array of forks with signal set arrays, 
of which every set gets executed procedurally.

```
Example add instruction w/ signals:
"add b a" : [
    FORKDESC(null),
    FORK(0) {
        [ b->ACCUMULATOR ],
        [ a->ALU_IN, alu_add, ACCUMULATOR->b ]
    }
]
Example conditional jump:
"jne PREV" : [
    FORKDESC(FLAG_EQUAL),
    FORK(0) {
        [ARG->INSTR_CNT]
    },
    FORK(1) {
        [INSTR_CNT++]
    }
]
```

**Step 2.** A set of signals is transformed.  
Set is verified to contain only one write request for each memory endpoint.
Set is divided into subsets where memory endpoints do not overlap
over other sets. 
Then, each subset is ordered in sequence of memory transfers.  
_This step can and will be preprocessed to save processing power_

```
Registers: [A] [B] [C] [D] [E] [F] [G] [B1] [B2]
Available signals: (a->b) (b->c) (c->a) (a->c) (c->b) (b->a) ...

Example valid set:
a->b, b->c
a->b, b->a    # Circular reference is valid, because
              # registers are read at a point in time

Example invalid set:
a->b, c->b    # Reason: multiple write assignments

Example subset division:
a->b, a->c, d->e, f->g
{a->b, a->c}, {d->e}, {f->g}

Example subset ordering:
d->e, a->b, c->d, b->c
a->b, b->c, c->d, d->e

B1->B2, B2->b, a->B1
a->B1, B1->B2, B2->b
```

**Step 3.** Signals in subsets are executed.  
Memory transfers are realized sequentially.  
_This step could be split into multiple threads, but emulator only benefits from uncommon instructions containing signals that can be split into big amounts of subsets._

**Step 4.** Repeat.  
Emulator does not provide any built-in instruction counter. 
It is on the architecture to provide a way to move to the next code segment,
eg. incrementing arch-specific instruction register using a signal.