import os
import json
import random
from faker import Faker
from datetime import datetime

# Initialize Faker
fake = Faker()

# Specify the number of records to generate
num_records = 1000  # Adjust as needed

# Generate a list of unique InquiryNumbers between 10000 and 99999
inquiry_numbers = random.sample(range(10000, 100000), num_records)

# Define datetime boundaries
start_raised = datetime(2022, 1, 1)
end_date_boundary = datetime(2024, 12, 31)

# Paths to JSON files
client_json_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\Clients.json"
user_role_links_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\UserRoleLinks.json"

# Load client aliases from Clients.json
with open(client_json_path, "r", encoding="utf-8") as f:
    clients_data = json.load(f)
client_aliases = [client["Alias"] for client in clients_data if "Alias" in client]

# Load user names from UserRoleLinks.json
with open(user_role_links_path, "r", encoding="utf-8") as f:
    user_role_links = json.load(f)
user_names = [link["UserName"] for link in user_role_links if "UserName" in link]

records = []

for inq_num in inquiry_numbers:
    # Generate RaisedDate and CreatedDate
    raised_date = fake.date_time_between(start_date=start_raised, end_date=end_date_boundary)
    created_date = fake.date_time_between_dates(datetime_start=raised_date, datetime_end=end_date_boundary)
    
    # Generate random Header and Description
    header = fake.sentence(nb_words=6)
    description = fake.text(max_nb_chars=200)
    
    # Random PriorityID
    priority_id = random.randint(1, 5)
    
    # Select a random ClientAlias from the list
    client_alias = random.choice(client_aliases) if client_aliases else ""
    # Select a random UserName from the list for CreatedBy
    created_by = random.choice(user_names) if user_names else ""
    
    # Create a record dictionary
    record = {
        "InquiryNumber": str(inq_num),
        "RaisedDate": raised_date.isoformat(),
        "Header": header,
        "Description": description,
        "PriorityID": priority_id,
        "CreatedDate": created_date.isoformat(),
        "ClientAlias": client_alias,
        "StatusID": 1,
        "Type": "Bug",
        "CreatedByAlias": created_by
    }
    records.append(record)

# Determine script directory and ensure "Generated" subfolder exists
script_dir = os.path.dirname(os.path.abspath(__file__))
generated_folder = os.path.join(script_dir, "Generated")
os.makedirs(generated_folder, exist_ok=True)

# Define output JSON file path
output_path = os.path.join(generated_folder, "Bugs.json")

# Write the records to a JSON file
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(records, f, ensure_ascii=False, indent=4)

print(f"Generated {num_records} records and saved to {output_path}")
