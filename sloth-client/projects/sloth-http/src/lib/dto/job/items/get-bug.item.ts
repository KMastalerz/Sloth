export interface GetBugItem {
    jobID: number;
    header: string;
    description: string;
    type: string;
    createdDate: Date;
    updatedDate?: Date | null;
    closedDate?: Date | null;
    isClosed: boolean;
    currentOwner?: GetUserBugItem | null;
    currentTeam?: GetTeamBugItem | null;
    createdBy?: GetUserBugItem | null;
    closedBy?: GetUserBugItem | null;
    client?: GetClientBugItem | null;
    updatedBy?: GetUserBugItem | null;
    priority?: GetPriorityBugItem | null;
    status?: GetStatusBugItem | null;
    comments: GetCommentBugItem[];
    assignmentHistory: GetAssignmentHistoryBugItem[];
    assignments: GetAssignmentBugItem[];
    files: GetFileBugItem[];
    priorityHistory: GetPriorityHistoryBugItem[];
    statusHistory: GetStatusHistoryBugItem[];
    products: GetProductBugItem[];
    functionalities: GetFunctionalityBugItem[];
}

export interface GetAssignmentBugItem {
    assignedDate: Date;
    user: GetUserBugItem;
    team: GetTeamBugItem;
    assignedBy: GetUserBugItem;
}

export interface GetAssignmentHistoryBugItem {
    changedDate: Date;
    previousOwner: GetUserBugItem;
    currentOwner: GetUserBugItem;
    changedBy: GetUserBugItem;
    team: GetTeamBugItem;
}

export interface GetClientBugItem {
    name: string;
    alias: string;
}

export interface GetCommentBugItem {
    commentID: number;
    comment: string;
    commendDate: Date;
    isEdited: boolean;
    commentedBy: GetUserBugItem;
    previousEdits?: GetCommentBugItem[] | null;
}

export interface GetFileBugItem {
    fileID: string;
    name: string;
    size: number;
    extension: string;
    addedDate: Date;
    addedBy: GetUserBugItem;
}

export interface GetFunctionalityBugItem {
    functionalityID: number;
    name: string;
    tag: string;
    tagColor?: string | null;
    description: string;
}

export interface GetPriorityBugItem {
    tag: string;
    tagColor?: string | null;
    description?: string | null;
}

export interface GetPriorityHistoryBugItem {
    changedDate: Date;
    changedBy: GetUserBugItem;
    newPriority: GetPriorityBugItem;
}

export interface GetProductBugItem {
    productID: number;
    alias: string;
    name: string;
    description?: string | null;
}

export interface GetStatusBugItem {
    tag: string;
    tagColor?: string | null;
    description?: string | null;
}

export interface GetStatusHistoryBugItem {
    changedDate: Date;
    changedBy: GetUserBugItem;
    newStatus: GetStatusBugItem;
}

export interface GetTeamBugItem {
    alias: string;
    speciality: string;
    name: string;
    description: string;
}

export interface GetUserBugItem {
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
}
