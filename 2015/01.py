file_path = "2015/inputs/01.txt"

with open(file_path, "r") as file:
    file_contents = file.read()

floor = 0
position = 0
found_basement = False

for char in file_contents:
    floor += 1 if char == '(' else -1 if char == ')' else 0

    if not found_basement: position += 1
    if floor < 0 and not found_basement: found_basement = True

print(floor)
print(position)