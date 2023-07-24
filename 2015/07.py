def find_value(var_name):
    if (var_name.isdigit()):
       return int(var_name)
    else:
        if var_name not in results:
            words = inputs[var_name]
            print(words)
            if len(words) == 1:
                val = find_value(words[0])
            else:
                if words[-2] == 'AND':
                    val = find_value(words[0]) & find_value(words[2])
                elif words[-2] == 'OR':
                    val = find_value(words[0]) | find_value(words[2])            
                elif words[-2] == 'LSHIFT':
                    val = find_value(words[0]) << find_value(words[2])
                elif words[-2] == 'RSHIFT':
                    val = find_value(words[0]) >> find_value(words[2])
                elif words[-2] == 'NOT':
                    val = ~find_value(words[1]) & 0xFFFF
                
            results[var_name] = val
        return results[var_name]

file_path = "2015/inputs/07.txt"

with open(file_path, "r") as file:
    file_contents = file.readlines()

inputs = {}
results = {}

for line in file_contents:
    (instruction, name) = line.split('->')
    inputs[name.strip()] = instruction.strip().split()

inputs['b'] = ['3176']

print(find_value('a'))