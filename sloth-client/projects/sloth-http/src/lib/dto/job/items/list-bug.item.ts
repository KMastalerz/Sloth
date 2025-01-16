export interface ListBugResponse {
    totalCount: number,
    bugs: ListBugItem[]
}
export interface ListBugItem {
    bugID: number;
    inquiryNumber?: string;
    header: string;
    description: string;
    priorityID: number;
    priority?: ListBugPriorityItem;
    status?: ListBugStatusItem;
    type: string;
    createdDate: Date;
    updatedDate?: Date;
    closedDate?: Date;
    isClosed: boolean;
    isBlocker: boolean;
    currentOwnerID?: string;
    currentOwner?: string;
    currentTeamID?: string;
    currentTeam?: string;
    updatedByID?: string;
    updatedBy?: string;
    closedByID?: string;
    closedBy?: string;
    clientID?: string;
    client?: string;
    products?: ListBugProductItem[];
    functionalities?: ListBugFunctionalityItem[];
}

export interface ListBugProductItem {
    name: string,
    decription: string
}

export interface ListBugFunctionalityItem {
    tag: string,
    tagColor: string
    decription: string
}

export interface ListBugPriorityItem {
    tag: string,
    tagColor: string
}

export interface ListBugStatusItem {
    tag: string,
    tagColor: string
}