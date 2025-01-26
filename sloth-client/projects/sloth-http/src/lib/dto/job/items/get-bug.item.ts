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

    assignments: GetAssignmentBugItem[];
    childJobs: GetJobLinkBugItem[];
    comments: GetCommentBugItem[];
    files: GetFileBugItem[];
    functionalities: GetFunctionalityBugItem[];
    parentJobs: GetJobLinkBugItem [];
    products: GetProductBugItem[];
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

  export interface GetJobLinkBugItem {
    parentJobID: number;
    parentJobHeader: number;
    parentJobDescription: number;
    childJobID: number;
    childJobHeader: number;
    childJobDescription: number;
    linkDate: string;
    linkedBy: string;
    linkedByFullName: string;
    linkedByEmail: string;
  }


  