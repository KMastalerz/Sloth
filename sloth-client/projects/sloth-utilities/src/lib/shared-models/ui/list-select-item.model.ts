export interface ListSelectItem {
    value: string;
    label?: string | null;
    group?: string | null;
}

export interface ListItemGroup {
    name: string;
    items: ListSelectItem[];
}