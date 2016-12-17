push ECX
; XvSettable
mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA0]

mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA1]

mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA2]

mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA3]

mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA4]

mov  dword ECX, 0xffffff
sub  dword ECX, [XV_GAMMA5]

;LSBFirst
mov  dword EAX, 0x00
push dword EAX, [X_LSB]
mov  dword EAX, 0x00
push dword EAX, [V_LSB]
mov  dword EAX, 0x00
push dword EAX, [X_LSB]
mov  dword EAX, 0x10
push dword EAX, [V_LSB]
mov  dword EAX, 0x71
push dword EAX, [C_LSB]
mov  dword EAX, 0xAA
push dword EAX, [M_LSB]