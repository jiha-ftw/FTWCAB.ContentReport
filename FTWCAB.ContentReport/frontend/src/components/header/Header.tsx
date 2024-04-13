import './Header.css';
import { useEffect } from 'react';
import Select from 'react-select'
import { ContentType } from '@/data/data';
import { useAppSelector, useAppDispatch } from '@/hooks'
import { fetchContentTypeGroups, setSelectedType } from '@/store/contentTypes'

const Header = () => {
  const dispatch = useAppDispatch();
  const contentTypeGroups = useAppSelector(state => state.contentTypes).types;

  useEffect(() => {
    dispatch(fetchContentTypeGroups());
  }, [dispatch])

  return (
    <div className="epi-main-header Header">
      <header className="axiom-typography--header1">
        Content Usages
      </header>
      <div style={{ width: '300px' }}>
        <Select
          isClearable
          placeholder="Select content type"
          getOptionLabel={(option: ContentType) => option.name}
          getOptionValue={(option: ContentType) => option.id.toString()}
          options={contentTypeGroups}
          onChange={(o) => dispatch(setSelectedType(o))}
        />
      </div>
    </div>
  );
}

export default Header;
