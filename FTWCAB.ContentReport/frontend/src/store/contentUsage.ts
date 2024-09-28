import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import type { RootState } from '@/store/configureStore'
import type { ContentUsages, ContentUsage, ContentInstance } from '@/data/data'

export interface ContentUsageState {
    usages: ContentUsage[],
    selectedContentInstance: ContentInstance | null,
    loaded: Boolean,
    totalCount: number,
    pages: number,
    pageSize: number,
}

const initialState: ContentUsageState = {
    usages: [],
    selectedContentInstance: null,
    loaded: false,
    totalCount: 0,
    pages: 0,
    pageSize: 10,
}

const fetchContentTypeUsages = createAsyncThunk
    <ContentUsages, {
        contentInstanceId: Number;
        page: Number;
        languageId: String;
    }>('contentTypes/fetchContentInstanceUsages', async ({ contentInstanceId, languageId, page }, { getState }) => {
        const { contentUsage: { pageSize } } = getState() as RootState;
        const params = new URLSearchParams({
            'contentInstanceId': contentInstanceId.toString(),
            'languageId': languageId.toString(),
            'page': page.toString(),
            'pageSize': pageSize.toString(),
        });
        const response = await fetch(`/EPiServer/FTWCAB.ContentReport/ContentUsagesApi/GetContentUsages?${params.toString()}`);
        return (await response.json());
    });

export const contentUsageSlice = createSlice({
    name: 'contentUsage',
    initialState,
    reducers: {
        setSelectedContentInstance: (state, action) => {
            state.selectedContentInstance = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchContentTypeUsages.pending, (state, action) => {
            state.loaded = false;
        });
        builder.addCase(fetchContentTypeUsages.fulfilled, (state, action) => {
            state.loaded = true;
            state.usages = action.payload.usages;
            state.totalCount = action.payload.totalCount;
            state.pages = action.payload.pages;
        });
        builder.addCase(fetchContentTypeUsages.rejected, (state, action) => {
            state.loaded = false;
        });
    },
})

export { fetchContentTypeUsages };
export const { setSelectedContentInstance } = contentUsageSlice.actions;
export const usages = (state: RootState) => state.contentUsage.usages
export const selectedContentInstance = (state: RootState) => state.contentUsage.selectedContentInstance

export default contentUsageSlice.reducer
