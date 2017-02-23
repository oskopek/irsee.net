cat rfc1459.txt | grep -E '^[ ]+[0-9]{3}[ ]+[A-Z_]+$' | sort -n | sed -E 's/^[ ]+//; s/[ ]+$//; s/ [ ]+/ /g; s/([0-9]+) ([A-Z_]+)/\2 = \1,/' > error_codes.txt
