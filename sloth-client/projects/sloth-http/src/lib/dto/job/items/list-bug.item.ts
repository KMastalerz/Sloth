export interface ListBugItem {
    bugID: number;
    inquiryNumber?: string;
    header: string;
    description: string;
    priorityID: number;
    priority?: string;
    status?: string;
    type: string;
    createdDate: Date;
    updatedDate?: Date;
    closedDate?: Date;
    isClosed: boolean;
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
    products?: string[];
    functionalities?: string[];
}