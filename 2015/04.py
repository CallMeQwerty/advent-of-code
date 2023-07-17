import hashlib 

file_path = "2015/inputs/04.txt"

with open(file_path, "r") as file:
    file_contents = file.read()

num = 0
hash_result = ""

while (hash_result[0:6] != "000000"):
    num += 1
    input_string = file_contents + str(num)
    md5_hash = hashlib.md5()
    input_bytes = input_string.encode('utf-8')
    md5_hash.update(input_bytes)
    hash_result = md5_hash.hexdigest()


print(num)
print(hash_result)