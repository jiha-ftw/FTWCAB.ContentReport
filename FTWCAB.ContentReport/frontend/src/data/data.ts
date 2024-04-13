export interface ContentType {
    readonly id: Number;
    readonly name: string;
    readonly fullName: string;
}

export interface ContentGroupType {
    readonly label: string;
    readonly options: ContentType[];
}

export interface ContentInstance {
    readonly id: number;
    readonly name: string;
    readonly editLink: string;
    readonly parentContentTypeName: string;
    readonly parentEditLink: string;
    readonly parentName: string;
}

export interface ContentInstances {
    readonly instances: ContentInstance[];
    readonly totalCount: number;
    readonly pages: number;
}

export interface ContentUsage {
    readonly name: string;
}

export interface ContentUsages {
    readonly usages: ContentUsage[];
    readonly totalCount: number;
    readonly pages: number;
}
