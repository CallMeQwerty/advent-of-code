import ast

file_path = "2015/inputs/08.txt"

char_count = 0
with open(file_path, "r") as file:
    for line in file:
        char_count += len(line.strip())

memory_count = 0
with open(file_path, "r") as file:
    for line in file:
        decoded_line = ast.literal_eval(line.strip())
        memory_count += len(decoded_line)  

encoded_count = 0
with open(file_path, "r") as file:
    for line in file:
        original_code = line.strip()
        encoded_string = '"' + original_code.replace('\\', '\\\\').replace('"', '\\"') + '"'
        encoded_count += len(encoded_string)

print(f"Char count: {char_count}")
print(f"Memory count: {memory_count}")
print(f"Encoded char count: {encoded_count}")
print(f"Part 1: {char_count - memory_count}")
print(f"Part 2: {encoded_count - char_count}")