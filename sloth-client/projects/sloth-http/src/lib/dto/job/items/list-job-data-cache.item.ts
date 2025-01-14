import { ListSelectItem, ToggleListItem } from "sloth-utilities";


export interface ListJobDataCacheItem {
    clients: ListSelectItem[];
    jobPriorities: ToggleListItem[];
    products: ListSelectItem[];
}