import sys
import pandas as pd
import os

def json_files_to_excel(output_excel, json_files):
    # Create an ExcelWriter object to write multiple sheets to one Excel file
    with pd.ExcelWriter(output_excel, engine='openpyxl') as writer:
        for json_file in json_files:
            try:
                # Read the JSON file into a DataFrame
                df = pd.read_json(json_file)
            except ValueError as e:
                print(f"Error reading {json_file}: {e}")
                continue

            # Use the base file name as the sheet name, truncated to 31 characters maximum
            base_name = os.path.splitext(os.path.basename(json_file))[0]
            sheet_name = base_name[:31]  # Excel sheet names limited to 31 characters

            # Write DataFrame to a separate sheet in the Excel file
            df.to_excel(writer, sheet_name=sheet_name, index=False)

    print(f"Combined Excel file saved as '{output_excel}'.")

if __name__ == "__main__":
    # Check if the script received enough arguments
    if len(sys.argv) < 3:
        print("Usage: python json_to_excel.py output.xlsx file1.json file2.json ...")
        sys.exit(1)

    # First argument is the output Excel file name
    output_excel = sys.argv[1]
    # The rest of the arguments are paths to JSON files
    json_files = sys.argv[2:]

    json_files_to_excel(output_excel, json_files)