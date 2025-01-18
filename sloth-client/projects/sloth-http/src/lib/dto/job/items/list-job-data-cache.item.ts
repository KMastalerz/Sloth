import { ListSelectItem, ToggleListItem } from "sloth-utilities";


export interface ListJobDataCacheItem {
    clients: ListSelectItem[];
    priorities: ToggleListItem[];
    products: ListSelectItem[];
    functionalities: ListSelectItem[];
}