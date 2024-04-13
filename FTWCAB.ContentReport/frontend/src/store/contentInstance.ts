import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import type { RootState } from '@/store/configureStore'
import type { ContentInstance, ContentInstances } from '@/data/data'

export interface ContentTypesState {
    instances: ContentInstance[]
    loaded: Boolean,
    totalCount: number,
    pages: number,
    pageSize: number,
}

const initialState: ContentTypesState = {
    instances: [],
    loaded: false,
    totalCount: 0,
    pages: 0,
    pageSize: 10,
}

const fetchContentTypeInstances = createAsyncThunk
    <ContentInstances, {
        contentTypeId: Number;
        page: Number;
    }>('contentTypes/fetchContentTypeInstances', async ({ contentTypeId, page }, { getState }) => {
        const { contentInstances: { pageSize } } = getState() as RootState;
        const response = await fetch(`/EPiServer/FTWCAB.ContentReport/ContentUsagesApi/GetContentInstances?contentTypeId=${contentTypeId}&page=${page}&pageSize=${pageSize}`);
        return (await response.json());
    });

export const contentInstancesSlice = createSlice({
    name: 'contentInstance',
    initialState,
    reducers: { },
    extraReducers: (builder) => {
        builder.addCase(fetchContentTypeInstances.pending, (state, action) => {
            state.loaded = false;
        });

        builder.addCase(fetchContentTypeInstances.fulfilled, (state, action) => {
            state.loaded = true;
            state.instances = action.payload.instances;
            state.totalCount = action.payload.totalCount;
            state.pages = action.payload.pages;
        });
        builder.addCase(fetchContentTypeInstances.rejected, (state, action) => {
            state.loaded = false;
        });
    },
})

export { fetchContentTypeInstances };
export const pages = (state: RootState) => state.contentInstances.pages
export const loaded = (state: RootState) => state.contentInstances.loaded
export const totalCount = (state: RootState) => state.contentInstances.totalCount
export const pageSize = (state: RootState) => state.contentInstances.pageSize

export default contentInstancesSlice.reducer
