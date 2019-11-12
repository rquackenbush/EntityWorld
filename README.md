# EntityWorld

Okay - so we want a more flexible instruction / execution structure.

First implementation:
- A set of hardcoded instructions that are very domain specific. 

Second attempt:
- Want something something a bit more generic.
- What about storing all instructions and memory in one contiguous stucture?
    - Could do self modifying stuff (could end up having mostly useless programs)?
    - Do we want some sort of "safer" stucture? We want to support self modification, but we want it to be a somewhat "safe" operation.
    - Could do a "fragile" implementation to start with to get the ball rolling.
    - Do we want to separate the memory from the instructions?