export interface ListBugParam {
    pageID: number;
    takeCount: number;
    currentOwner?: string | null;
    header?: string | null;
    description?: string | null;
    isClosed?: boolean | null;
    bugID?: number | null;
    inquiryNumber?: string | null;
    createdDateStart?: Date | null;
    createdDateEnd?: Date | null;
    updatedDateStart?: Date | null;
    updatedDateEnd?: Date | null;
    closedDateStart?: Date | null;
    closedDateEnd?: Date | null;
    raisedDateStart?: Date | null;
    raisedDateEnd?: Date | null;
    products?: number[] | null;
    functionalities?: number[] | null;
    clients?: string[] | null;
  }