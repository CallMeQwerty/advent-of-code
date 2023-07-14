
file_path = "2015/inputs/01.txt"
file = open(file_path, "r")

file_contents = file.read()

floor = 0
position = 0
found_basement = False

for item in file_contents:
    if item == '(': floor += 1
    if item == ')': floor -= 1

    if found_basement == False: position += 1
    if floor < 0 and found_basement == False: found_basement = True
  
file.close()

print(floor)
print(position)