; get ammount memory
push dword EBP 
mov  dword EBP, ESP 
sub  dword ESP, 0x4 
; ------
push dword EBP 
mov  dword EBP, ESP 
mov  dword EAX, [MultiBootInfo_Memory_High] 
xor  dword EDX, EDX 
mov  dword ECX, 0x400 
div  dword ECX 
add  dword EAX, 0x1 
push dword EAX 
; ------
test dword ECX, 0x2 
JE   near  EM 
add  dword ESP, 0x4 
JNE  near  EM