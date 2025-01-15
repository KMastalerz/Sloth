import os
import json
import random
import pandas as pd
import warnings
from faker import Faker  # Included per request

# Paths to input JSON files
bug_product_link_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\JobProductLinks.json"
product_functionalities_path = r"D:\Repo\Sloth\sloth-services\sloth.API\DataSeed\ProductFunctionalities.json"

# Load JSON files into pandas DataFrames
bug_product_link_df = pd.read_json(bug_product_link_path)
product_func_df = pd.read_json(product_functionalities_path)

# Merge tables on ProductAlias
merged_df = pd.merge(bug_product_link_df, product_func_df, on="ProductAlias", how="inner")

# Define a function to randomly sample functionalities for each group
def sample_functionalities(group):
    n = len(group)
    if n == 0:
        return group
    k = random.randint(1, n)
    return group.sample(n=k)

# Temporarily suppress DeprecationWarning for the groupby/apply operation
with warnings.catch_warnings():
    warnings.filterwarnings("ignore", category=DeprecationWarning)
    # Group by JobHeader, apply sampling, and reset index
    grouped = (merged_df
               .groupby("JobHeader", group_keys=False)
               .apply(sample_functionalities)
               .reset_index(drop=True))

# Rename "Name" to "FunctionalityName" for the output
result_df = grouped[["JobHeader", "Name"]].rename(
    columns={"Name": "FunctionalityName"}
)

# Convert the DataFrame to a list of dictionaries
result_records = result_df.to_dict(orient="records")

# Determine script directory and ensure "Generated" subfolder exists
script_dir = os.path.dirname(os.path.abspath(__file__))
generated_folder = os.path.join(script_dir, "Generated")
os.makedirs(generated_folder, exist_ok=True)

# Define output JSON file path
output_path = os.path.join(generated_folder, "JobFunctionalityLinks.json")

# Write the JSON list to the output file
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(result_records, f, ensure_ascii=False, indent=4)

print(f"Generated JSON file saved to {output_path}")
