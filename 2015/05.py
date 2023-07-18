file_path = "2015/inputs/05.txt"

with open(file_path, "r") as file:
    file_contents = file.readlines()

nice_string_count = 0

for lane in file_contents:

    # Pair letters
    has_pair = False
    for i in range(len(lane) - 1):
        pair = lane[i:i+2] 
        if lane.count(pair) >= 2 and pair not in lane[i+2:]:
            has_pair = True
            break

    # Repeating letter
    has_repeating_letter = False
    for i in range(len(lane) - 2):
        if lane[i] == lane[i+2]:
            has_repeating_letter = True
            break

    if has_pair and has_repeating_letter:
        nice_string_count += 1

print(nice_string_count)


    