import os
import json
import random
import pandas as pd
import warnings
from faker import Faker  # Included as per original request

# Paths to input JSON files
bug_product_link_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\JobProductLinks.json"
product_functionalities_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\ProductFunctionalities.json"

# Load JSON files into pandas DataFrames
bug_product_link_df = pd.read_json(bug_product_link_path)
product_func_df = pd.read_json(product_functionalities_path)

# Ensure necessary columns are present in the data
if "ProductAlias" not in bug_product_link_df.columns or "ProductAlias" not in product_func_df.columns:
    raise KeyError("Both JobProductLinks and ProductFunctionalities must contain 'ProductAlias'.")

if "Name" not in product_func_df.columns:
    raise KeyError("ProductFunctionalities must contain a 'Name' column for functionality names.")

# Merge JobProductLinks with ProductFunctionalities on ProductAlias
merged_df = pd.merge(bug_product_link_df, product_func_df, on="ProductAlias", how="inner")

# Define a function to randomly sample functionalities for each bug (job)
def sample_functionalities(group):
    n = len(group)
    if n == 0:
        return pd.DataFrame(columns=group.columns)  # Return empty DataFrame if no functionalities
    k = random.randint(1, min(5, n))  # Select between 1 and 5 functionalities
    return group.sample(n=k)

# Temporarily suppress DeprecationWarning for the groupby/apply operation
with warnings.catch_warnings():
    warnings.filterwarnings("ignore", category=DeprecationWarning)
    # Group by JobHeader and sample functionalities
    grouped = (merged_df
               .groupby("JobHeader", group_keys=False)
               .apply(sample_functionalities)
               .reset_index(drop=True))

# Select only the necessary columns and rename for the output
result_df = grouped[["JobHeader", "Name"]].rename(columns={"Name": "FunctionalityName"})

# Convert the resulting DataFrame to a list of dictionaries
result_records = result_df.to_dict(orient="records")

# Determine script directory and ensure "Generated" subfolder exists
script_dir = os.path.dirname(os.path.abspath(__file__))
generated_folder = os.path.join(script_dir, "Generated")
os.makedirs(generated_folder, exist_ok=True)

# Define output JSON file path
output_path = os.path.join(generated_folder, "JobFunctionalityLinks.json")

# Write the list of functionality links to the JSON file
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(result_records, f, ensure_ascii=False, indent=4)

print(f"Generated JSON file saved to {output_path}")
