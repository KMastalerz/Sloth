export interface ListJobDataCacheItem {
    clients: CacheClientItem[];
    priorities: CachePriorityItem[];
    products: CacheProductItem[];
    functionalities: CacheFunctionalityItem[];
    statuses: CacheStatusItem[];
    assignees: CacheAssigneeItem[];
}

export interface CacheClientItem {
    name: string;
    clientID: string;
}

export interface CachePriorityItem {
    priorityID: number;
    tag: string;
    tagColor?: string | null;
}

export interface CacheProductItem {
    productID: number;
    description: string | null;
    name: string;
}

export interface CacheFunctionalityItem {
    functionalityID: number;
    name: string;
    tag: string;
    description: string | null;
    tagColor?: string | null;
    product?: string | null;
}

export interface CacheStatusItem {
    statusID: number;
    type: string;
    tag: string;
    tagColor: string | null;
    description?: string | null;
}

export interface CacheAssigneeItem {
    userName: string;
    fullName: string;
    team: string;
    email: string;
}