;redcode-94b
;name <horchata>
;author Adrian Alvarez
;strategy Los 7 enanitos pero no son enanitos, son imps
;version 1

istep  equ (CORESIZE+1)/7

        spl    1                 
        spl    1
        spl    1
        spl    1
        spl    2
launch  jmp    imp
        add.a  #istep,     launch
imp     mov.i  #0,         istep