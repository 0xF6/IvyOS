start:   
                   jmp start_program
                   nop
   
oemname            db 'ETS'
bytespersector     dw 512
sectorspercluster  db 1
ressectors         dw 1
numcopiesfat       db 2
maxallocrootdir    dw 224
maxsectors         dw 2880 ;for 1.44 mbytes disk
mediadescriptor    db 0f0h ;fd = 2 sides 18 sectors
sectorsperfat      dw 9
sectorspertrack    dw 18
heads              dw 2
hiddensectors      dd 0
hugesectors        dd 0 ;if sectors > 65536
drivenumber        db 0
                   db 0
bootsignature      db 029h ;extended boot signature
volumeid           dd 0