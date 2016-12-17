start_program:
  xor  AX, AX
  mov  SS, AX
  mov  SP, bps
  push SS
  pop  DS
  mov  SI, loading+bps