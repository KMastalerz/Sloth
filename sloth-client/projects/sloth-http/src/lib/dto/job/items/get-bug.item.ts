export interface GetBugItem {
    jobID: number;
    header: string;
    description: string;
    type: string;
    createdDate: Date;
    updatedDate: Date | null;
    closedDate: Date | null;
    isClosed: boolean;
    isBlocker: boolean;
    createdBy: GetUserBugItem | null;
    closedBy: GetUserBugItem | null;
    client: GetClientBugItem | null;
    updatedBy: GetUserBugItem | null;
    priority: GetPriorityBugItem | null;
    status: GetStatusBugItem | null;
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
    assignedTo: string;
    assignedToFullName: string;
    assignedToEmail: string;
    assignedBy: string;
    assignedByFullName: string;
    assignedByEmail: string;
  }
  
  export interface GetAssignmentHistoryBugItem {
    changedDate: Date;
    previousOwner: string;
    previousOwnerFullName: string;
    previousOwnerEmail: string;
    currentOwner: string;
    currentOwnerFullName: string;
    currentOwnerEmail: string;
    changedBy: string;
    changedByFullName: string;
    changedByEmail: string;
  }
  
  export interface GetClientBugItem {
    clientID: string;
    name: string;
    alias: string;
  }
  
  export interface GetCommentBugItem {
    commentID: number;
    comment: string;
    commentDate: Date;
    isEdited: boolean;
    commentedBy: string;
    commentedByEmail: string;
    commentedByFullName: string;
  }
  
  export interface GetFileBugItem {
    fileID: string; 
    name: string;
    size: number;
    extension: string;
    addedDate: Date;
    addedBy: string;
    addedByEmail: string;
    addedByFullName: string;
  }
  
  export interface GetFunctionalityBugItem {
    functionalityID: number;
    name: string;
    tag: string;
    tagColor: string | null;
    description: string;
  }
  
  export interface GetPriorityBugItem {
    priorityID: number,
    tag: string;
    tagColor: string | null;
    description: string | null;
  }
  
  export interface GetPriorityHistoryBugItem {
    changedDate: Date;
    changedBy: string;
    changedByEmail: string;
    changedByFullName: string;
    newPriorityTag: string;
    newPriorityTagColor: string | null;
    newPriorityDescription: string | null;
  }
  
  export interface GetProductBugItem {
    productID: number;
    alias: string;
    name: string;
    description: string | null;
  }
  
  export interface GetStatusBugItem {
    statusID: number;
    tag: string;
    tagColor: string | null;
    description: string | null;
  }
  
  export interface GetStatusHistoryBugItem {
    changedDate: Date;
    changedBy: string;
    changedByEmail: string;
    changedByFullName: string;
    newStatusTag: string;
    newStatusTagColor: string | null;
    newStatusDescription: string | null;
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
  