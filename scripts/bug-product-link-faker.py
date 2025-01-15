import os
import json
import random
import pandas as pd
import warnings

# Paths to input JSON files
products_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\Products.json"
client_products_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\ClientProducts.json"
clients_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\Clients.json"
bugs_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\Bugs.json"

# Load JSON files into pandas DataFrames
products_df = pd.read_json(products_path)
client_products_df = pd.read_json(client_products_path)
clients_df = pd.read_json(clients_path)
bugs_df = pd.read_json(bugs_path)

# Join tables according to the specified relationships:
# 1. Join Bugs with Clients on ClientAlias
bugs_clients = pd.merge(bugs_df, clients_df, left_on="ClientAlias", right_on="Alias", suffixes=("_bug", "_client"))

# 2. Join the result with ClientProducts on Clients.Alias = ClientProducts.ClientAlias
bugs_clients_cp = pd.merge(bugs_clients, client_products_df, left_on="ClientAlias", right_on="ClientAlias")

# 3. Join with Products on ClientProducts.ProductAlias = Products.Alias
full_merge = pd.merge(bugs_clients_cp, products_df, left_on="ProductAlias", right_on="Alias", suffixes=("", "_product"))

# Define a function to randomly sample products for each group
def sample_products(group):
    n = len(group)
    if n == 0:
        return group
    # Randomly decide how many products to sample for this bug
    k = random.randint(1, n)
    return group.sample(n=k)

# Temporarily suppress DeprecationWarning for the groupby/apply operation
with warnings.catch_warnings():
    warnings.filterwarnings("ignore", category=DeprecationWarning)
    grouped = full_merge.groupby("Header", group_keys=False) \
                      .apply(sample_products) \
                      .reset_index(drop=True)

# Select relevant columns and rename for output
result_df = grouped[["Header", "ProductAlias"]].rename(columns={"Header": "JobHeader"})

# Convert the DataFrame to a list of dictionaries
result_records = result_df.to_dict(orient="records")

# Determine script directory and ensure "Generated" subfolder exists
script_dir = os.path.dirname(os.path.abspath(__file__))
generated_folder = os.path.join(script_dir, "Generated")
os.makedirs(generated_folder, exist_ok=True)

# Define output JSON file path
output_path = os.path.join(generated_folder, "JobProductLinks.json")

# Write the JSON list to the output file
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(result_records, f, ensure_ascii=False, indent=4)

print(f"Generated JSON file saved to {output_path}")
