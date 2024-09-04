var content = """
CT~~CD,~CC^~CT~
^XA
^DFR:labelprint.ZPL^FS
~TA000
~JSN
^LT0
^MNW
^MTT
^PON
^PMN
^LH0,0
^JMA
^PR6,6
~SD15
^JUS
^LRN
^CI27
^PA0,1,1,0
^MMT
^PW969
^LL378
^LS0
^BY7,3,214^FT131,265^BCN,,Y,N
^FH\^FD>:@#SnNumber^FS

^XZ

""";

Regex.Match(content, "(?<=\\^DFR:)(?=\\^FS)").Dump();