def move(pos, char):
    if char == '^':
        pos += 1j  # Move up (complex number addition)
    elif char == '>':
        pos += 1   # Move right
    elif char == 'v':
        pos -= 1j  # Move down (complex number subtraction)
    elif char == '<':
        pos -= 1   # Move left
    return pos

file_path = "2015/inputs/03.txt"

with open(file_path, "r") as file:
    file_contents = file.read()

santa = 0
robot = 0
positions = {0}  # Using set to store unique positions
turn = True

for char in file_contents:
    if turn:
        santa = move(santa, char)
        positions.add(santa)
    else:
        robot = move(robot, char)
        positions.add(robot)
    turn = not turn

print(len(positions))

   
