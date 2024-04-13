import { configureStore } from '@reduxjs/toolkit'
import contentTypes from './contentTypes'
import contentInstances from './contentInstance';
import contentUsage from './contentUsage';

const store = configureStore({
  reducer: {
    contentTypes,
    contentInstances,
    contentUsage,
  }
})

export default store;

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
